package com.stasiuk.program;

import org.junit.Test;

import java.util.HashSet;

import static org.junit.Assert.*;

public class MainTest {

    @Test
    public void main() {
        final HashSet<Integer> aHashStructure = new HashSet<Integer>();

        for(int i = 0; i < 10000000; i++) aHashStructure.add(i);

        long s = System.currentTimeMillis();

        for(int i = 0; i< 10000000; i++) {
            int aElement = i;
            assertTrue(aHashStructure.contains(aElement));
        }

        long f = System.currentTimeMillis();

        System.out.println("Time =" + (f-s));

        s = System.currentTimeMillis();

        int aElement2 = 0;
        for(int i = 0; i< 10000000; i++) assertTrue(aHashStructure.contains(aElement2));

        f = System.currentTimeMillis();

        System.out.println("Time =" + (f-s));
    }
}