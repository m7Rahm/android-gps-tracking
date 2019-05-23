package com.example.myapplication.Classes.AdapterClasses;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.TextView;
import com.example.myapplication.Classes.UserClasses.CarInfoClass;
import com.example.myapplication.Interfaces.NavigateToObjectInterface;
import com.example.myapplication.R;

import java.util.List;

public class RecyclerViewAdapter extends RecyclerView.Adapter<RecyclerViewAdapter.MyViewHolder> {
    private List<CarInfoClass> carInfoClasses;
    private NavigateToObjectInterface navigateToObjectInterface;
    public RecyclerViewAdapter(List<CarInfoClass> carInfoClasses, NavigateToObjectInterface navigateToObjectInterface){
        this.carInfoClasses = carInfoClasses;
        this.navigateToObjectInterface = navigateToObjectInterface;
    }
    @NonNull
    @Override
    public RecyclerViewAdapter.MyViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
       View view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.recycler_view_content,viewGroup,false);
        return new RecyclerViewAdapter.MyViewHolder(view);
    }
    @Override
    public void onBindViewHolder(@NonNull RecyclerViewAdapter.MyViewHolder viewHolder, int i) {
        viewHolder.button.setOnClickListener(v -> {
            if(!navigateToObjectInterface.ShowHideMarker(i))
            viewHolder.button.setImageResource(R.drawable.ic_hide_eye_vector);
            else
                viewHolder.button.setImageResource(R.drawable.ic_show_eye_vector);
        });
        viewHolder.carName.setOnClickListener(v-> navigateToObjectInterface.NavigateTo(i));
        viewHolder.carName.setText(carInfoClasses.get(i).getCarPlate());
    }
    @Override
    public int getItemCount() {
        return carInfoClasses.size();
    }
    class MyViewHolder extends RecyclerView.ViewHolder{
        TextView carName;
        ImageButton button;
        MyViewHolder(@NonNull View itemView) {
            super(itemView);
            button = itemView.findViewById(R.id.imageB);
            carName = itemView.findViewById(R.id.carPlateTV);
        }
    }
}

