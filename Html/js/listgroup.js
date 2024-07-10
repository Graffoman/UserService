async function loadlistgroup() {
    let url = "https://localhost:5101/api/Group/list";

    let response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" }
    });

    if (response.ok === true) {
        let groups = await response.json();
        let grouprows = document.getElementById("grouptbody");
        groups.forEach(group => grouprows.append(grouprow(group)));
    }
    else {
        let error = await response.json();
        console.log(error.message);
    }

}

function grouprow(group) {

    let tr = document.createElement("tr");
    tr.setAttribute("data-rowid", group.id);

    let nameTd = document.createElement("td");
    nameTd.append(group.name);
    tr.append(nameTd);

    /*
    let linksTd = document.createElement("td");
 
    const removeLink = document.createElement("button");
    removeLink.append("Удалить");
    removeLink.addEventListener("click", async () => await deleteGroup(group.id));
 
    linksTd.append(removeLink);
    tr.appendChild(linksTd);
    */

    let linksTd = document.createElement("td");

    const editLink = document.createElement("button");
    editLink.append("Подробнее");
    editLink.className = "button";
    editLink.addEventListener("click", async () => await editgroup(group.id));

    linksTd.append(editLink);
    tr.appendChild(linksTd);

    return tr;
}

function editgroup(groupid) {
    localStorage.setItem('groupid', groupid);
    window.location.href = 'group.html';
}

document.getElementById("addgroupbutton").addEventListener("click", async () => {
    window.location.href = 'newgroup.html';
});

window.onload = function () {
    loadlistgroup();
}   
