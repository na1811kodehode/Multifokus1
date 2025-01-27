function clickityClack() {
    console.log("This is to test clickityClack function");

    //Grab HTML elements..
    let inputD = document.getElementById("inputDegrees").valueAsNumber;
    let unit1D = document.getElementById("unit1").value;
    let unit2D = document.getElementById("unit2").value;

    //Validate input
    if(isNaN(inputD)) {
        alert("Only numbers are allowed!");
        return;
    }

    const formData = new FormData();
    formData.append("inputStr", inputD);
    formData.append("unit1", unit1D);
    formData.append("unit2", unit2D);

    fetch("http://localhost:5208/tempconv", {
        method: "POST",
        body: formData
    })
    .then((response) => {
        if (!response.ok) {
            return response.text().then((errorMessage) => {
                alert(errorMessage);
                throw new Error(errorMessage);
            });
        }
        return response.json();
    })
    .then((data) => {
        console.log(data.message);

        document.getElementById("output").value = data.message;

        //Reset fields..
        document.getElementById("inputDegrees").value = "";
        document.getElementById("unit1").selectedIndex = 0;
        document.getElementById("unit2").selectedIndex = 0;
    })
    .catch((error) => {
        console.error("error", error);
    });
}