package com.example.myapplication.Classes;


import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class RetrofitClass {
    private static String url;
    public RetrofitClass(String url) {
        RetrofitClass.url = url;
    }
   public Retrofit getRetrofit(){
        return new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
    }
}
