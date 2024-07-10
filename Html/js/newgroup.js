
async function savegroupinfo(savegroupid, groupname) {
    let url = "https://localhost:5101/api/Group/";

    let response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            name: groupname
        })
    });

    if (response.ok === true) {
        console.log("Данные сохранены!");
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }

    location.href = 'listgroup.html';
}

document.getElementById("savebutton").addEventListener("click", async () => {
    let groupname = document.getElementById("groupname").value;
    await savegroupinfo(groupid, groupname);
});

