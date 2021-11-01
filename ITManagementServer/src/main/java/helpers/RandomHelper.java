package helpers;

import java.util.Random;

public class RandomHelper {
    public static int Randomize() {
        Random rand = new Random();

        return rand.nextInt(64000 - 12000) + 12000;
    }
}