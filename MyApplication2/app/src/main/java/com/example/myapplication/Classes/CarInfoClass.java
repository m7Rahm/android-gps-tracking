package com.example.myapplication.Classes;


import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class CarInfoClass implements Serializable {
    @SerializedName("lat")
    private double lat;
    @SerializedName("lng")
        private double lng;
    @SerializedName("speed2")
    private float speed;
    @SerializedName("branch_id")
    private int branchId;
    @SerializedName("object_id")
    private int objectId;
    @SerializedName("insert_date")
    private String lastInfoTime;
    @SerializedName("car_model")
    private String carModel;
    @SerializedName("object_num")
    private String numberPlate;

    public double getLat() {
        return lat;
    }

     void setLat(double lat) {
        this.lat = lat;
    }

    public double getLng() {
        return lng;
    }

     void setLng(double lng) {
        this.lng = lng;
    }

    public float getSpeed() {
        return speed;
    }

     void setSpeed(float speed) {
        this.speed = speed;
    }

    public int getObjectId() {
        return objectId;
    }

     void setObjectId(int objectId) {
        this.objectId = objectId;
    }

    public int getBranchId() {
        return branchId;
    }

     void setBranchId(int branchId) {
        this.branchId = branchId;
    }

    public String getLastInfoTime() {
        return lastInfoTime;
    }

     void setLastInfoTime(String lastInfoTime) {
        this.lastInfoTime = lastInfoTime;
    }

    public String getCarModel() {
        return carModel;
    }

     void setCarModel(String carModel) {
        this.carModel = carModel;
    }

    public String getCarPlate() {
        return numberPlate;
    }

     void setCarPlate(String numberPlate) {
        this.numberPlate = numberPlate;
    }
}
