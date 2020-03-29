package com.example.myapplication.Interfaces;

import com.example.myapplication.Classes.UserClasses.CarInfoClass;
import com.example.myapplication.Classes.UserClasses.UserDataClass;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface RetrofitInterface {
    @GET("api/objectsLocation/{rowNum}/{branchId}")
    Call<List<CarInfoClass>> getInitialCoordinates(@Path("rowNum") int rowNum,@Path("branchId") int branchId);
    @GET("api/objectsLocation/{branchId}")
    Call<List<CarInfoClass>> getUpdates(@Path("branchId") int branchId);
    @GET("api/login/{userName}")
    Call<UserDataClass> getLoginInfo (@Path("userName") String userName);
}
