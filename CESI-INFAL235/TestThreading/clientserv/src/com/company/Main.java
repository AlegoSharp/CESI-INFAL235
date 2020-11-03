package com.company;

import java.io.IOException;

public class Main {

    public static void main(String[] args) throws IOException {

        long sTemps = System.nanoTime();

        int nbClient = 100000;

        Server s = new Server();
        new Thread(s).start();

        for(int i = 0; i < nbClient; i++){
            Client c = new Client(s);
            new Thread(c).start();
        }

        long eTemps = System.nanoTime();
        System.out.println((eTemps - sTemps)/1000000000);
    }
}
