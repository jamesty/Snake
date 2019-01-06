package snake;

import java.util.Timer;
import java.util.TimerTask;
import javafx.application.Application;
import javafx.event.EventHandler;
import javafx.scene.Scene;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;
import javafx.scene.layout.StackPane;
import javafx.scene.text.Font;
import javafx.scene.text.Text;
import javafx.stage.Stage;

public class Snake extends Application {
    
    @Override
    public void start(Stage primaryStage) {
        StackPane root = new StackPane();
        Scene scene = new Scene(root, 1024, 768);
        Canvas canvas = new Canvas(1024, 768);

        GraphicsContext gc = canvas.getGraphicsContext2D();
        // Record player movement (player will be intially moving right)
        boolean[] movement = new boolean[4];
        for (int i = 0; i < 4; i++) {
            movement[i] = false;
        }
        movement[3] = true;
        SnakeBlock player = new SnakeBlock(32, 0, 2);
        player.addBlock();
        player.draw(gc);

        Food food = Food.makeFood(canvas, player);
        
        // Display game over message when player dies.
        Text gameover = new Text(580, 580, "Game Over");
        gameover.setFont(new Font(20));
        gameover.setVisible(false);
        
        // Game loop
        Timer timer = new Timer();
        timer.schedule(new TimerTask() {
            @Override
            public void run() {
                // Clear the canvas so we can display the next frame.
                gc.clearRect(0, 0, canvas.getWidth(), canvas.getHeight());
                player.move(movement);
                player.draw(gc);
                player.eat(canvas, food);
                Food.draw(gc, canvas, food.x, food.y);
                if (player.collisionCheck(canvas)) {
                    timer.cancel();
                    gc.clearRect(0, 0, canvas.getWidth(), canvas.getHeight());
                    gameover.setVisible(true);
                }
            }
        }, 500, 100);
        
        // Key press event handler
        scene.setOnKeyPressed(new EventHandler<KeyEvent>() {
            @Override
            public void handle(KeyEvent event) {
                
                if (event.getCode() == KeyCode.UP && !movement[1]) {
                    for (int i = 0; i < 4; i++) {
                        movement[i] = false;
                    }
                    movement[0] = true;
                } else if (event.getCode() == KeyCode.DOWN && !movement[0]) {
                    for (int i = 0; i < 4; i++) {
                        movement[i] = false;
                    }
                    movement[1] = true;
                } else if (event.getCode() == KeyCode.LEFT && !movement[3]) {
                    for (int i = 0; i < 4; i++) {
                        movement[i] = false;
                    }
                    movement[2] = true;
                } else if (event.getCode() == KeyCode.RIGHT && !movement[2]) {
                    for (int i = 0; i < 4; i++) {
                        movement[i] = false;
                    }
                    movement[3] = true;
                }
            }
        });
        root.getChildren().add(gameover);
        root.getChildren().add(canvas);
        
        primaryStage.setTitle("Snake");
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }
    
}
