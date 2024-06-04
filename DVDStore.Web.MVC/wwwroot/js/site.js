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


