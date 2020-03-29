package com.example.myapplication.Classes.AdapterClasses;

import android.annotation.SuppressLint;
import android.app.Dialog;
import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.graphics.drawable.Drawable;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.helper.ItemTouchHelper;
import android.view.View;

import com.example.myapplication.R;

import static android.support.v7.widget.helper.ItemTouchHelper.ACTION_STATE_SWIPE;

public class SwipeMenu extends ItemTouchHelper.SimpleCallback {
    private ColorDrawable backgroundColor;
    private Drawable drawable;
    private Context context;

    public SwipeMenu(@NonNull Context context) {
        super(0, ItemTouchHelper.RIGHT);
        this.context = context;
        backgroundColor = new ColorDrawable();
        drawable =  context.getDrawable(R.drawable.ic_information_icon);
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
        return 1.2f;
    }

    @Override
    public void onSwiped(@NonNull RecyclerView.ViewHolder viewHolder, int i) {

    }

    @Override
    public int getSwipeDirs(@NonNull RecyclerView recyclerView, @NonNull RecyclerView.ViewHolder viewHolder) {
        return super.getSwipeDirs(recyclerView, viewHolder);
    }

    @SuppressLint("ClickableViewAccessibility")
    @Override
    public void onChildDraw(@NonNull Canvas c, @NonNull RecyclerView recyclerView, @NonNull RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, boolean isCurrentlyActive) {
        View itemView = viewHolder.itemView;
        drawable.setBounds(itemView.getLeft(), itemView.getTop()-10,  120, itemView.getBottom());
            c.clipRect(itemView.getLeft(), itemView.getTop(), itemView.getLeft()+ dX/5, itemView.getBottom());
        //View menuView =
        //isMenuVisible = dX > 110;
        drawable.draw(c);
        recyclerView.setOnTouchListener((v,event)->{
            if (!isCurrentlyActive && event.getX()<120) {
                recyclerView.setClickable(false);
                //Toast.makeText(context, String.valueOf(event.getX()), Toast.LENGTH_LONG).show();
                Dialog dialog = new Dialog(context);
                dialog.setContentView(R.layout.activity_login);
                dialog.show();
            }
            else recyclerView.setClickable(true);
            return false;
        });
        if(actionState ==ACTION_STATE_SWIPE)
        super.onChildDraw(c, recyclerView, viewHolder, dX/5, 0, actionState, isCurrentlyActive);
    }
}
