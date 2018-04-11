# CRC Remainder Calculator

## What is CRC?
> A cyclic redundancy check (CRC) is an error-detecting code commonly used in digital networks and storage devices to detect accidental
> changes to raw data. 
> Blocks of data entering these systems get a short check value attached, based on the remainder of a polynomial division of their
> contents. On retrieval, the calculation is repeated and, in the event the check values do not match, corrective action can be taken
> against data corruption (Source: Wikipedia)

## Introduction

This is a simple application to compute and check an n-bit binary CRC remainder.

## Usage and Computation Example

### Get the CRC remainder

`string remainder = CalculateRemainder(message, polynom);`

### Check the CRC remainder

`string result = CheckCRCMessage(message, polynom, remainder);`

### Computation Example

In this example, we shall encode 8 bits of message with a 4-bit CRC, with a polynomial x³ + x² + 1. The polynomial is written in binary as the coefficients (1x³ + 1x² + 0x + 1) a 3rd-order polynomial has 4 coefficients (1x³ + 1x² + 0x + 1). In this case, the coefficients are 1, 1, 0 and 1. The result of the calculation is 3 bits long.

Start with the message to be encoded:

> 10011010

This is first padded with zeros corresponding to the bit length n of the CRC. Here is the first calculation for computing a 3-bit CRC:

> 10011010 000 <--- input right padded by 3 bits
> 1101         <--- divisor (4 bits) = x³ + x² + 1

The algorithm acts on the bits directly above the divisor in each step. The result for that iteration is the bitwise XOR of the polynomial divisor with the bits above it. The bits not above the divisor are simply copied directly below for that step. The divisor is then shifted one bit to the right, and the process is repeated until the divisor reaches the right-hand end of the input row. Here is the entire calculation:

![Example Of CRC Remainder Calculation](https://github.com/DanielShefer/CRC-Remainder-Calculator/blob/master/CRC_Calculation.PNG)
