using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

[Serializable]
public class StageInfo
{
    public string id;
    public string width;
    public string height;

    public string time;

    public string step;
    public string tile;
    public string clear;

    public string initBlock;
    private GameTile[] tiles;


   // private 
    public StageInfo(int id,int width,int height,int time,int step,int clear,int initBlock,GameTile[] tiles)
    {
        this.id = id+"";
        this.width = width+"";
        this.height = height+"";
         this.time = time+"";
          this.step = step+"";
          this.clear = clear+"";
            this.initBlock = initBlock+"";
        this.tiles = tiles;
        StringBuilder builder = new StringBuilder();
        int maxIndex = width*height;
       	for (int i = 0, y = 0; y < height; y++) {
			for (int x = 0; x < width; x++, i++) {
               
                    GameTile tile = tiles[i];
                    int type = (int)tile.Content.Type;
                   Debug.Log(tile.Content.Type);
                    builder.Append(type);
                    if(i<maxIndex-1){
                         builder.Append(" ");
                    }
                
			}
		}
        this.tile = builder.ToString();
    }

    //pravate Save(person);
}
