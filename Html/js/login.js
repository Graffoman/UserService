async function login(useremail, userpassword) {

    let response = await fetch("https://localhost:5101/api/User/login", {
        method: "POST",
        headers: {"Content-Type": "application/json" },
        body: JSON.stringify({
            email: useremail,
            password: userpassword
        })
});

if (response.ok === true) {
    let user = await response.json();
    document.getElementById("userid").value = user.id;
    localStorage.setItem('userid', user.id);
    window.location.href = 'userform.html';
    }
 else {
    let error = await response.json();
    console.log(error.message);
    }
}

document.getElementById("loginbutton").addEventListener("click", async () => {
    let useremail = document.getElementById("emaileditor").value;
    let userpassword = document.getElementById("passwordeditor").value;

    await login(useremail, userpassword);
});
