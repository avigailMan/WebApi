let user = {
    userid: 0,
    email: "",
    password: "",
    firstName: "",
    lastName: "",
}

async function passwordChecking() {
    try {
        const password = document.getElementById("psw").value;
        const response = await fetch("api/User/password", {
            method: 'POST',
            headers: {
                'Content-Type':"application/json"
            },
            body: JSON.stringify(password)
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        
        let color = document.getElementById("passwordCheck");
        if (data == 0) {
            color.style.setProperty("background-color", "red");
        } else if (data == 1) {
            color.style.setProperty("background-color", "orange");
        } else if (data >= 2) {
            color.style.setProperty("background-color", "green");
        }
    } catch (error) {
        console.error("Error:", error);
    }
}

async function addUser() {
    try {
        user.email = document.getElementById("email").value;
        user.password = document.getElementById("psw").value;
        user.firstName = document.getElementById("firstName").value;
        user.lastName = document.getElementById("lastName").value;

        if (user.email == "" || user.password == "") {
            return alert("Email and password required");
        }

        const response = await fetch("api/User", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });

        if (!response.ok) {
            let validEmail = document.getElementById("email");
            validEmail.style.display = "block";
            throw new Error('Network response was not ok');
        }

        const data = await response.json();
        console.log(data);
        alert(`Welcome ${user.firstName}, you are inside:)`);
    } catch (error) {
        console.error("Error:", error);
    }
}

async function login() {
    try {
        user.email = document.getElementById("emailLogin").value;
        user.password = document.getElementById("pswLogin").value;
        user.firstName = document.getElementById("firstName").value;
        user.lastName = document.getElementById("lastName").value;
        if (user.email == "" || user.password == "") {
            return alert("Email and password required");
        }

        const response = await fetch("api/User/login", {
            method: "POST",
            headers: {
                'Content-Type': "application/json"
            },
            body: JSON.stringify(user),
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();
        sessionStorage.setItem("user", JSON.stringify(data));
        window.location.replace("Products.html");
    } catch (error) {
        console.error("Error:", error);
    }
}

async function updateUser() {
    try {
        userStorage = JSON.parse(sessionStorage.getItem("user"));
        
        user.userid = parseInt(userStorage.userid);

        user.email = document.getElementById("email").value;
        user.password = document.getElementById("psw").value;
        user.firstName = document.getElementById("firstName").value;
        user.lastName = document.getElementById("lastName").value;
        

        console.log(user);

        const response = await fetch("api/User", {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Network response was not ok: ${errorText}`);
        }
    } catch (error) {
        console.error("Error:", error);
    }
    alert(user.firstName+", Your detailes where changed")
    window.location.replace("Products.html");
}