package com.gillabs.memophysics;

import android.app.Activity;
// import android.content.Context;
import android.os.Bundle;
import android.util.Log;

import com.esafirm.imagepicker.features.ImagePicker;

public class ImageManager  {

    private static final String TAG = "ImageManager";

    public static void pickPhotos(Activity activity)
    {
        Log.d(TAG, "pickPhotos: ");

        ImagePicker.create(activity)
            // .returnMode(ReturnMode.ALL) // set whether pick and / or camera action should return immediate result or not.
            // .folderMode(true) // folder mode (false by default)
            // .toolbarFolderTitle("Folder") // folder selection title
            // .toolbarImageTitle("Tap to select") // image selection title
            // .toolbarArrowColor(Color.BLACK) // Toolbar 'up' arrow color
            // .single() // single mode
            // .multi() // multi mode (default mode)
            // .limit(10) // max images can be selected (99 by default)
            // .showCamera(true) // show camera or not (true by default)
            // .imageDirectory("Camera") // directory name for captured image  ("Camera" folder by default)
            // .origin(images) // original selected images, used in multi mode
            // .exclude(images) // exclude anything that in image.getPath()
            // .excludeFiles(files) // same as exclude but using ArrayList<File>
            // .theme(R.style.CustomImagePickerTheme) // must inherit ef_BaseTheme. please refer to sample
            // .enableLog(false) // disabling log
            // .imageLoader(new GrayscaleImageLoder()) // custom image loader, must be serializeable
            .start(); // start image picker activity with request code
    }
}
