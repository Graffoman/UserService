async function saveroleinfo(saveroleid, rolename) {
    let url = "https://localhost:5101/api/Role/";

    let response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            name: rolename
        })
    });

    if (response.ok === true) {
        console.log("Данные сохранены!");
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }

    location.href = 'listrole.html';
}

document.getElementById("savebutton").addEventListener("click", async () => {
    let name = document.getElementById("rolename").value;
    await saveroleinfo(roleid, name);   
});

document.getElementById("adduserbutton").addEventListener("click", async () => {
    window.location.href = 'addusertorole.html';
});