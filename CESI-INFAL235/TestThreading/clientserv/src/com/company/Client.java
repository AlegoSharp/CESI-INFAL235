package com.company;

public class Client implements Runnable {

    private Server s;

    public Client(Server s){
        this.s = s;
    }

    public void run(){
        try {
            demande();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public void demande() throws InterruptedException {
        System.out.println(s.date());
    }
}
