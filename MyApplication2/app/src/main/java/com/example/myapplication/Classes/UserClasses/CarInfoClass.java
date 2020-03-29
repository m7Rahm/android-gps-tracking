package com.example.myapplication.Classes.UserClasses;


import android.os.Parcel;
import android.os.Parcelable;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class CarInfoClass implements Serializable, Parcelable {
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
    private boolean isExpanded = false;
    public  CarInfoClass()
    {
    }

    private CarInfoClass(Parcel in) {
        lat = in.readDouble();
        lng = in.readDouble();
        speed = in.readFloat();
        branchId = in.readInt();
        objectId = in.readInt();
        lastInfoTime = in.readString();
        carModel = in.readString();
        numberPlate = in.readString();
    }

    public static final Creator<CarInfoClass> CREATOR = new Creator<CarInfoClass>() {
        @Override
        public CarInfoClass createFromParcel(Parcel in) {
            return new CarInfoClass(in);
        }

        @Override
        public CarInfoClass[] newArray(int size) {
            return new CarInfoClass[size];
        }
    };

    public double getLat() {
        return lat;
    }

     public void setLat(double lat) {
        this.lat = lat;
    }

    public double getLng() {
        return lng;
    }

     public void setLng(double lng) {
        this.lng = lng;
    }

    public float getSpeed() {
        return speed;
    }

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    public int getObjectId() {
        return objectId;
    }

    public void setObjectId(int objectId) {
        this.objectId = objectId;
    }

    public int getBranchId() {
        return branchId;
    }

     public void setBranchId(int branchId) {
        this.branchId = branchId;
    }

    public String getLastInfoTime() {
        return lastInfoTime;
    }

    public void setLastInfoTime(String lastInfoTime) {
        this.lastInfoTime = lastInfoTime;
    }

    public String getCarModel() {
        return carModel;
    }

    public void setCarModel(String carModel) {
        this.carModel = carModel;
    }

    public String getCarPlate() {
        return numberPlate;
    }

    public void setCarPlate(String numberPlate) {
        this.numberPlate = numberPlate;
    }

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeDouble(lat);
        dest.writeDouble(lng);
        dest.writeFloat(speed);
        dest.writeInt(branchId);
        dest.writeInt(objectId);
        dest.writeString(lastInfoTime);
        dest.writeString(carModel);
        dest.writeString(numberPlate);
    }

    public boolean isExpanded() {
        return isExpanded;
    }

    public void setExpanded(boolean expanded) {
        isExpanded = expanded;
    }
}
