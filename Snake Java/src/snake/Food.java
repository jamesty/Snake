package snake;

import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

public class Food {
    int x, y;
    private Food(int randx, int randy) {
        this.x = randx;
        this.y = randy;
    }
    
    /**
     * Creates a food object at a random location on the canvas.
     * @param canvas Canvas object where the food will be placed.
     * @param player The SnakeBlock head of the player.
     * @return Food object containing a random x and y value.
     */
    public static Food makeFood(Canvas canvas, SnakeBlock player) {
        int randx = 0, randy = 0;
        Food food = new Food(randx, randy);
        Food.changeLocation(canvas, food, player);
        return food;
    }
    
    public static void changeLocation(Canvas canvas, Food food, SnakeBlock player) {
        food.x = (int) (32 * Math.floor((Math.random() * (canvas.getWidth() + 1)) / 32));
        food.y = (int) (32 * Math.floor((Math.random() * (canvas.getHeight() + 1)) / 32));
        
        // Make sure that the food does not spawn inside the player.
        SnakeBlock current = player;
        while (current.next != null) {
            if (food.x == current.x && food.y == current.y) {
                current = null;
                Food.changeLocation(canvas, food, player);
            }
            current = current.next;
        }
    }
    
    public static void draw(GraphicsContext gc, Canvas canvas, int x, int y) {
        gc.setFill(Color.RED);
        gc.fillRect(x, y, 32, 32);
    }
}
