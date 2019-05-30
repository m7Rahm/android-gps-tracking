package com.example.myapplication.Activities;

import java.io.Serializable;
import java.security.*;

import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.myapplication.Classes.UserClasses.CarInfoClass;
import com.example.myapplication.Classes.ConnectionClass;
import com.example.myapplication.Classes.RetrofitClass;
import com.example.myapplication.Classes.UserClasses.UserDataClass;
import com.example.myapplication.Interfaces.RetrofitInterface;
import com.example.myapplication.R;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends AppCompatActivity {
    List<CarInfoClass> carInfoClass;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        //RetrofitClass retrofitClass = new RetrofitClass();
        final Context context = this;
        final EditText usernameET = findViewById(R.id.usernameET);
        final EditText passwordET = findViewById(R.id.passwordET);
        Button button = findViewById(R.id.submitButton);
        final Handler handler = new Handler();
        button.setOnClickListener((v -> new Thread(()->{
            if (!usernameET.getText().toString().equals("")&& !passwordET.getText().toString().equals("")) {
                UserDataClass userDataClass = new ConnectionClass().getLoginInfo("GetLoginInfo", usernameET.getText().toString());
                if (encodePassword(passwordET.getText().toString(), userDataClass.getUserPassword())) {
                    //ArrayList<CarInfoClass> carInfoClass = new ConnectionClass().getInitialCoordinates("GetInitialCoordinates", 1, userDataClass.getUserId());
                    RetrofitInterface retrofit =  RetrofitClass.getRetrofit("http://10.1.11.134/RESTWebService/").create(RetrofitInterface.class);
                    Call<List<CarInfoClass>> call = retrofit.getInitialCoordinates(1,userDataClass.getUserId());
                    call.enqueue(new Callback<List<CarInfoClass>>() {
                        @Override
                        public void onResponse(@NonNull Call<List<CarInfoClass>> call, @NonNull Response<List<CarInfoClass>> response) {
                            if(response.isSuccessful())
                            carInfoClass = response.body();
                            Intent intent = new Intent(context, MainActivity.class);
                            intent.putExtra("carInfoClass", (Serializable) carInfoClass);
                            intent.putExtra("userDataClass",userDataClass);
                            startActivity(intent);
                        }

                        @Override
                        public void onFailure(@NonNull Call<List<CarInfoClass>> call, @NonNull Throwable t) {
                            Toast.makeText(context,"Error connecting to server",Toast.LENGTH_LONG).show();
                        }
                    });
                }
                else
                    handler.post(()-> Toast.makeText(context,"Wrong Credentials",Toast.LENGTH_LONG).show());
            }
            else handler.post(()-> Toast.makeText(context,"Login or Password field is empty",Toast.LENGTH_LONG).show());
        }).start()));
    }
    private boolean encodePassword(@NonNull String inputString, String encodedString)  {
        try {

            MessageDigest md5Encoder = MessageDigest.getInstance("MD5");
            byte[] encodedData = md5Encoder.digest(inputString.getBytes());
            StringBuilder hexString = new StringBuilder();
            for (byte b : encodedData) {
                String hex=Integer.toHexString(0xff & b);
                if(hex.length()==1) hexString.append('0');
                hexString.append(hex);
            }
            return hexString.toString().equals(encodedString);
        }
         catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
        return false;
    }
}
