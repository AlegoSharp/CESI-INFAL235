package com.company;

import java.time.LocalDate;

public class Server implements Runnable{

    public void run(){
    }

    public LocalDate date(){
        return java.time.LocalDate.now();
    }

}
