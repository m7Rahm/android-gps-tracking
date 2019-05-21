package com.example.myapplication.Classes;

import java.io.Serializable;

public class UserDataClass implements Serializable {
    private int userId;
    private String username,userPassword,userEmail;

    UserDataClass() {
        userId = -1;
        username = "";
        userPassword = "";
        userEmail = "";
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

    public String getUserEmail() {
        return userEmail;
    }

    void setUserEmail(String userEmail) {
        this.userEmail = userEmail;
    }
}
