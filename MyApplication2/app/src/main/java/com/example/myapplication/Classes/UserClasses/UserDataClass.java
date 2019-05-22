package com.example.myapplication.Classes.UserClasses;

import java.io.Serializable;

public class UserDataClass implements Serializable {
    private int userId;
    private String username,userPassword,userEmail;

    public UserDataClass() {
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

    public void setUsername(String username) {
        this.username = username;
    }

    public String getUserPassword() {
        return userPassword;
    }

    public void setUserPassword(String userPassword) {
        this.userPassword = userPassword;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public String getUserEmail() {
        return userEmail;
    }

    public void setUserEmail(String userEmail) {
        this.userEmail = userEmail;
    }
}
