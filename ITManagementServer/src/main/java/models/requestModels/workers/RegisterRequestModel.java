package models.requestModels.workers;

public class RegisterRequestModel {
    private String Login;

    private String Password;

    private String AdministerPassword;

    public String getLogin() {
        return Login;
    }

    public void setLogin(String login) {
        Login = login;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public String getAdministerPassword() {
        return AdministerPassword;
    }

    public void setAdministerPassword(String administerPassword) {
        AdministerPassword = administerPassword;
    }
}