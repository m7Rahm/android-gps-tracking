package com.example.myapplication.Activities;

import android.os.Bundle;
import android.os.Handler;
import android.os.SystemClock;
import android.support.annotation.NonNull;
import android.support.v4.view.GravityCompat;
import android.support.v7.app.ActionBarDrawerToggle;
import android.view.MenuItem;
import android.support.design.widget.NavigationView;
import android.support.v4.widget.DrawerLayout;

import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.animation.AccelerateInterpolator;
import android.view.animation.Interpolator;
import android.view.animation.LinearInterpolator;

import com.example.myapplication.Classes.CarInfoClass;
import com.example.myapplication.Classes.UpdateLocationClass;
import com.example.myapplication.R;
import com.google.android.gms.maps.CameraUpdate;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;

import java.util.ArrayList;


public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener, OnMapReadyCallback {
    private volatile boolean [] hasUpdateFinished;
    private volatile LatLng latLng;
    private GoogleMap mMap;
    private Handler handler = new Handler();
    private ArrayList<CarInfoClass> carInfoClass;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        carInfoClass = (ArrayList<CarInfoClass>) getIntent().getSerializableExtra("carInfoClass");
        setContentView(R.layout.activity_main);
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        assert mapFragment != null;
        mapFragment.getMapAsync(this);
        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        NavigationView navigationView = findViewById(R.id.nav_view);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();
        navigationView.setNavigationItemSelectedListener(this);
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_home) {
            // Handle the camera action
        } else if (id == R.id.nav_share) {

        }
        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;
        // Add a marker in Sydney and move the camera
        //LatLng sydney = new LatLng(-34, 151);
        //CameraUpdate location = CameraUpdateFactory.newLatLngZoom(new LatLng(carInfoClass.get(0).getLat(),carInfoClass.get(0).getLng()), 10);
        //mMap.animateCamera(location);
        Marker [] markers = new Marker[carInfoClass.size()];
        hasUpdateFinished = new boolean[carInfoClass.size()];
        for (int i = 0; i <carInfoClass.size(); i++) {
            CarInfoClass currentItem = carInfoClass.get(i);
            markers [i] = mMap.addMarker(new MarkerOptions().position(new LatLng(currentItem.getLat(), currentItem.getLng())).title(currentItem.getCarPlate()));
            hasUpdateFinished[i] = true;
        }
        new Thread(()->
        {
            //float a = 45, b = 45;
            ArrayList<CarInfoClass> carInfoClasses;
            while (true)
            {
                try {
                    UpdateLocationClass updateLocationClass = new UpdateLocationClass();
                    carInfoClasses = updateLocationClass.getUpdates(3);
                    Thread.sleep(1);
                    if ((carInfoClasses.size() > 0)) {
                        for (int i = 0; i < carInfoClasses.size(); i++)
                            for(int j = 0; j < carInfoClass.size(); j++)
                            if(carInfoClass.get(j).getObjectId()==carInfoClasses.get(i).getObjectId())
                            if (hasUpdateFinished[j]) {
                                int currentElement = j;
                                latLng = new LatLng(carInfoClasses.get(i).getLat(), carInfoClasses.get(i).getLng());
                                hasUpdateFinished[currentElement] = false;
                                handler.post(() ->
                                        UpdateMarker(markers[currentElement], latLng, currentElement));
                            }
                    }
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }).start();
        //mMap.addMarker(new MarkerOptions().position(sydney).title("Marker in Sydney"));
        //mMap.moveCamera(CameraUpdateFactory.newLatLng(sydney));
    }
    public void UpdateMarker(Marker marker, LatLng finalPosition,int i)
    {
        final LatLng startPosition = marker.getPosition();
        final Handler handler = new Handler();
        //final long start = SystemClock.uptimeMillis();
        //final Interpolator interpolator = new LinearInterpolator();
        //final float durationInMs = 3000;
        double updateStepLat = finalPosition.latitude-startPosition.latitude;
        double updateStepLng = finalPosition.longitude-startPosition.longitude;

        //long elapsed;
//double t;
//double v;
        handler.post(() -> {
            // Calculate progress using interpolator
            //elapsed = SystemClock.uptimeMillis() - start;
            //t = elapsed / durationInMs;
            //v = interpolator.getInterpolation(t);
            for (int i1 = 0; i1 < 200; i1++)
            {
                LatLng currentPosition = new LatLng(marker.getPosition().latitude+updateStepLat,marker.getPosition().longitude+updateStepLng);
                marker.setPosition(currentPosition);
            }
            hasUpdateFinished[i] = true;
            // Repeat till progress is complete.
            //if (t < 1) {
                // Post again 16ms later.
                //handler.postDelayed(this, 16);
            //}
            //else
        });
    }
}
