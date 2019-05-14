package com.example.myapplication.Classes;

public class UserDataClass {
    private int userId,username,userPassword;

    public UserDataClass() {
    }

    public int getUserId() {
        return userId;
    }

    public int getUsername() {
        return username;
    }

    public void setUsername(int username) {
        this.username = username;
    }

    public int getUserPassword() {
        return userPassword;
    }

    public void setUserPassword(int userPassword) {
        this.userPassword = userPassword;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }
}
