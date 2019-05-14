package com.example.myapplication.Activities;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;

import com.example.myapplication.Classes.CarInfoClass;
import com.example.myapplication.Classes.ConnectionClass;
import com.example.myapplication.R;

import java.util.ArrayList;

public class LoginActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        Button button = findViewById(R.id.submitButton);
        button.setOnClickListener((v -> new Thread(()->{
        ArrayList<CarInfoClass> carInfoClass = new ConnectionClass("GetInitialCoordinates").getInitialCoordinates(1,3);
        Intent intent = new Intent(this,MainActivity.class);
        intent.putExtra("carInfoClass",carInfoClass);
        startActivity(intent);}).start()));
    }
}
