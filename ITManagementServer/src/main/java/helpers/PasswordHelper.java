package helpers;

import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.PBEKeySpec;
import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.util.Arrays;
import java.util.Base64;
import java.util.Random;

public class PasswordHelper {
    private static final int ITERATIONS = 10000;
    private static final int KEY_LENGTH = 256;
//    private static final String salt = "EqdmPh53c9x33EygXpTpcoJvc4VXLK";

    private static byte[] GenerateHash(char[] password, byte[] salt) {
        PBEKeySpec spec = new PBEKeySpec(password, salt, ITERATIONS, KEY_LENGTH);
        Arrays.fill(password, Character.MIN_VALUE);
        try {
            SecretKeyFactory skf = SecretKeyFactory.getInstance("PBKDF2WithHmacSHA1");
            return skf.generateSecret(spec).getEncoded();
        } catch (NoSuchAlgorithmException | InvalidKeySpecException e) {
            throw new AssertionError("Error while hashing a password: " + e.getMessage(), e);
        } finally {
            spec.clearPassword();
        }
    }

    public static String GenerateSalt() {
        int leftLimit = 65;
        int rightLimit = 122;
        int targetStringLength = 64;

        Random random = new Random();
        StringBuilder buffer = new StringBuilder(targetStringLength);
        for (int i = 0; i < targetStringLength; i++) {
            int randomLimitedInt = leftLimit + (int)
                    (random.nextFloat() * (rightLimit - leftLimit + 1));

            if (randomLimitedInt > 90 && randomLimitedInt < 97) {
                i--;
                continue;
            }

            buffer.append((char) randomLimitedInt);
        }
        String generatedString = buffer.toString();

        System.out.println(generatedString);

        return generatedString;
    }

    public static String GenerateSecurePassword(String password, String salt) {
        String returnValue = null;

        byte[] securePassword = GenerateHash(password.toCharArray(), salt.getBytes());

        returnValue = Base64.getEncoder().encodeToString(securePassword);

        return returnValue;
    }

    public static boolean VerifyUserPassword(String providedPassword, String salt, String securedPassword) {
        boolean returnValue = false;

        String newSecurePassword = GenerateSecurePassword(providedPassword, salt);

        returnValue = newSecurePassword.equalsIgnoreCase(securedPassword);

        return returnValue;
    }
}