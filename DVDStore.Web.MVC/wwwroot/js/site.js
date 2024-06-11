/**********************************************************************************
**
**  DVDStore Application v1.0
**
**  Copyright 2024
**  Developed by:
**     Ronald Garlit.
**
**  This software was created for educational purposes.
**
**  Use is subject to license terms.
***********************************************************************************
**
**  FileName: site.js (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**
**  Description:
**  This JavaScript file demonstrates several common coding issues that are typically flagged by
**  static code analysis tools like SonarLint. These include the use of magic numbers, the eval function,
**  unnecessary variables, debugging statements, improper type comparisons, potential cross-site scripting (XSS) vulnerabilities,
**  deprecated synchronous XMLHttpRequests, infinite recursion, uncontrolled modification of global variables,
**  and generic exception handling.
**
**  Specific issues include:
**  - Magic Numbers: Using hard-coded numeric values instead of named constants.
**  - eval Function: Using eval to execute code can lead to security vulnerabilities.
**  - Unused Variables: Declaring variables that are never used.
**  - Debugging Statements: Leaving console.log statements in production code.
**  - Type Coercion: Using == instead of ===, which can lead to unexpected type coercion.
**  - XSS Vulnerabilities: Directly assigning user input to innerHTML.
**  - Deprecated Practices: Using synchronous XMLHttpRequest which can block the main thread.
**  - Infinite Recursion: Creating a function that calls itself indefinitely.
**  - Global Variable Modification: Modifying global variables in an uncontrolled manner.
**  - Generic Exception Handling: Using catch blocks that do not properly handle exceptions.
**
**  Proper Use of site.js:
**  The `site.js` file in an ASP.NET Core MVC project is typically used to store custom JavaScript
**  code that enhances the interactivity and functionality of the website. This can include form
**  validation, dynamic content updates, AJAX calls, event handling, and more. It is important to
**  ensure that the code in `site.js` is clean, efficient, and secure to maintain the overall quality
**  and security of the web application. Additionally, bundling and minification of this file can
**  improve the performance of the website by reducing the number and size of HTTP requests.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-06-05      RGARLIT      CREATED FILE TO DEMONSTRATE COMMON CODING ISSUES
***********************************************************************************/

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var size = 4;       // Noncompliant

function calculateResult(expression) {
    // Using eval can lead to security risks, SonarLint will likely flag this
    let result = eval(expression);

    // Using == instead of === can lead to unexpected type coercion
    if (result == 0) {
        console.log("Result is zero.");
    }

    // Unused variable, SonarLint might flag this as unnecessary code
    let unused = "This variable is never used";

    // Debugging code should not be in production code
    console.log("Calculation complete.");

    return result;
}

// Example call to the function with potentially unsafe input
calculateResult("2 + 2");

function updateContent() {
    var userData = document.getElementById("userData").value;
    // Directly setting user input to innerHTML can lead to XSS
    document.getElementById("content").innerHTML = userData;
}

function loadSyncData() {
    var request = new XMLHttpRequest();
    // Synchronous XMLHttpRequest on the main thread is deprecated
    request.open('GET', 'data.json', false);
    request.send(null);
    console.log(request.responseText);
}

function recurseForever() {
    // Infinite recursion, will cause a stack overflow
    recurseForever();
}

var counter = 0;

function incrementCounter() {
    // Uncontrolled modification of global variable
    counter++;
    console.log("Counter is now: " + counter);
}

function fetchData(url) {
    try {
        var response = fetch(url);
        console.log(response.json());
    } catch (e) {
        // Generic catch block, which can swallow exceptions silently
        console.log("Something went wrong");
    }
}