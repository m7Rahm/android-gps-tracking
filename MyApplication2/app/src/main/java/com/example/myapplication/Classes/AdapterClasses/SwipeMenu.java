package com.example.myapplication.Classes.AdapterClasses;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.helper.ItemTouchHelper;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Toast;

public class SwipeMenu extends ItemTouchHelper.SimpleCallback {
    private ColorDrawable backgroundColor;
    Context context;
    public SwipeMenu(Context context) {
        super(0, ItemTouchHelper.RIGHT);
        this.context = context;
        backgroundColor = new ColorDrawable();
        backgroundColor.setColor(Color.parseColor("#b80f0a"));
    }
    @Override
    public boolean onMove(@NonNull RecyclerView recyclerView, @NonNull RecyclerView.ViewHolder viewHolder, @NonNull RecyclerView.ViewHolder viewHolder1) {
        return false;
    }

    @Override
    public int convertToAbsoluteDirection(int flags, int layoutDirection) {
        return super.convertToAbsoluteDirection(flags, layoutDirection);
    }

    @Override
    public float getSwipeThreshold(@NonNull RecyclerView.ViewHolder viewHolder) {
        return 1f;
    }

    @Override
    public void onSwiped(@NonNull RecyclerView.ViewHolder viewHolder, int i) {

    }

    @SuppressLint("ClickableViewAccessibility")
    @Override
    public void onChildDraw(@NonNull Canvas c, @NonNull RecyclerView recyclerView, @NonNull RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, boolean isCurrentlyActive) {
        View itemView = viewHolder.itemView;
        backgroundColor.setBounds(itemView.getLeft(), itemView.getTop(), itemView.getLeft() + ((int) dX/5), itemView.getBottom());
        //View menuView =
        recyclerView.setOnTouchListener((v, event) ->
        {
            if (event.getX() < 70){
                Toast.makeText(context, String.valueOf(event.getX()), Toast.LENGTH_LONG).show();
            return true;
        }
            else return false;
        });
        backgroundColor.draw(c);
        super.onChildDraw(c, recyclerView, viewHolder, dX/5, dY, actionState, isCurrentlyActive);
        // Draw the red delete background
    }
}
