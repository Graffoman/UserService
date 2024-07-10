
async function saveuserinfo(saveuserid, userlastName, username, usermiddleName, useremail, userbirthdayDate, userdepartment, userpassword ) {
    let url = "https://localhost:5101/api/User/";

    let response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            lastName: userlastName,
            name: username,
            middleName: usermiddleName,
            email: useremail,
            birthdayDate: userbirthdayDate,
            department: userdepartment,
            password: userpassword
        })
    });

    if (response.ok === true) {
        console.log("Данные сохранены");
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }

    location.href = 'listuser.html';
}

document.getElementById("savebutton").addEventListener("click", async () => {
    let lastName = document.getElementById("userlastname").value;
    let name = document.getElementById("username").value;
    let middleName = document.getElementById("usermiddlename").value;
    let email = document.getElementById("useremail").value;
    let birthdayDate = document.getElementById("userbirthdaydate").value;
    let department = document.getElementById("userdepartment").value;
    let password = document.getElementById("userpassword").value;
    let passwordrepeat = document.getElementById("userpasswordrepeat").value;
    if (password != passwordrepeat)
        alert("Не совпадают пароль и его повтор!")
    else await saveuserinfo(userid, lastName, name, middleName, email, birthdayDate, department, password);
});


