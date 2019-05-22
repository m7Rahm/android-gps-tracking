package com.example.myapplication.Classes.AdapterClasses;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.myapplication.Classes.UserClasses.CarInfoClass;
import com.example.myapplication.R;

import java.util.List;

public class RecyclerViewAdapter extends RecyclerView.Adapter<RecyclerViewAdapter.MyViewHolder> {
    List<CarInfoClass> carInfoClasses;
    RecyclerViewAdapter(List<CarInfoClass> carInfoClasses){
        this.carInfoClasses = carInfoClasses;
    }
    @NonNull
    @Override
    public RecyclerViewAdapter.MyViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
       View view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.recycler_view_content,viewGroup,true);
        return new RecyclerViewAdapter.MyViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerViewAdapter.MyViewHolder viewHolder, int i) {

    }

    @Override
    public int getItemCount() {
        return carInfoClasses.size();
    }
    class MyViewHolder extends RecyclerView.ViewHolder{

        MyViewHolder(@NonNull View itemView) {
            super(itemView);
        }
    }
}

