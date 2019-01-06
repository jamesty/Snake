package snake;

import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

public class SnakeBlock {
    int x, y, speed;
    SnakeBlock next;
    public SnakeBlock(int x, int y, int speed) {
        this.x = x;
        this.y = y;
        this.speed = speed;
        this.next = null;
    }
    
    public void draw(GraphicsContext gc) {
        gc.setFill(Color.BLUE);
        gc.fillRect(this.x, this.y, 32, 32);
        if (this.next != null) {
            this.next.draw(gc);
        }
    }
    
    public void addBlock() {
        SnakeBlock current = this;
        while (current.next != null) {
            current = current.next;
        }
        current.next = new SnakeBlock(current.x - 32, current.y, 2);
    }
    
    public void move(boolean[] movement) {
        // Move the whole snake body by placing the previous snake block's
        // position to the next snake block.
        if (this.next != null) {
            this.next.moveBody(this.x, this.y);
        }

        if (movement[0]) {
            this.y -= 32;
        } else if (movement[1]) {
            this.y += 32;
        } else if (movement[2]) {
            this.x -= 32;
        } else if (movement[3]) {
            this.x += 32;
        }
    }
    
    public void moveBody(int x, int y) {
        if (this.next != null) {
            this.next.moveBody(this.x, this.y);
        }
        this.x = x;
        this.y = y;
    }
    
    public void eat(Canvas canvas, Food food) {
        if (food.x == this.x && food.y == this.y) {
            Food.changeLocation(canvas, food, this);
            this.addBlock();
        }
    }
    
    public boolean collisionCheck(Canvas canvas) {
        // Check if snake has collided with end of canvas.
        if (this.x < 0 || this.y < 0 || this.y > canvas.getHeight() 
                || this.x > canvas.getWidth()) {
            return true;
        }
        
        // Check if snake has collided with itself.
        SnakeBlock current1 = this;
        while (current1.next != null) {
            SnakeBlock current2 = this;
            while (current2.next != null) {
                if (current1 != current2 && current1.x == current2.x && current1.y == current2.y) {
                    return true;
                }
                current2 = current2.next;
            }
            current1 = current1.next;
        }
        return false;
    }
}
