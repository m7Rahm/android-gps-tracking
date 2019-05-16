package com.example.myapplication.Classes;

public class UserDataClass {
    private int userId;
    private String username,userPassword;

    UserDataClass() {
        userId = -1;
        username = "";
        userPassword = "";
    }

    public int getUserId() {
        return userId;
    }

    public String getUsername() {
        return username;
    }

    void setUsername(String username) {
        this.username = username;
    }

    public String getUserPassword() {
        return userPassword;
    }

    void setUserPassword(String userPassword) {
        this.userPassword = userPassword;
    }

    void setUserId(int userId) {
        this.userId = userId;
    }
}
