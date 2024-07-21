async function loadlistuser() {
    let url = "https://localhost:5101/api/User/list";

    let response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" }
    });

    if (response.ok === true) {
        let users = await response.json();
        let userrows = document.getElementById("usertbody");
        users.forEach(user => userrows.append(userrow(user)));
    }
    else {
        let error = await response.json();
        console.log(error.message);
    }

}

async function deleteuser(deleteuserid) {
    let cr = confirm('Вы уверены, что хотите удалить пользователя?');
    if (cr) {
        let url = "https://localhost:5101/api/User/" + deleteuserid;

        let response = await fetch(url, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });

        if (response.ok === true) {
            location.reload();
        }
        else {
            let error = await response.json();
            console.log(error.message);
        }
    }

}

function userrow(user) {

    let tr = document.createElement("tr");
    tr.setAttribute("data-rowid", user.id);

    let lastnameTd = document.createElement("td");
    lastnameTd.append(user.lastName);
    tr.append(lastnameTd);

    let nameTd = document.createElement("td");
    nameTd.append(user.name);
    tr.append(nameTd);

    let middlenameTd = document.createElement("td");
    middlenameTd.append(user.middleName);
    tr.append(middlenameTd);

    let departmentTd = document.createElement("td");
    departmentTd.append(user.department);
    tr.append(departmentTd);

    let emailTd = document.createElement("td");
    emailTd.append(user.email);
    tr.append(emailTd);    

    let linksTd = document.createElement("td");

    const editLink = document.createElement("button");
    editLink.append("Подробнее");
    editLink.className = "button";
    editLink.addEventListener("click", async () => await edituser(user.id));

    linksTd.append(editLink);
    tr.appendChild(linksTd);

    let removelinksTd = document.createElement("td");

    const removeLink = document.createElement("button");
    removeLink.className = "deletebutton";
    removeLink.append("Удалить");
    removeLink.addEventListener("click", async () => await deleteuser(user.id));

    removelinksTd.append(removeLink);
    tr.appendChild(removelinksTd); 

    return tr;
}

UTF8 = {
    encode: function (s) {
        for (var c, i = -1, l = (s = s.split("")).length, o = String.fromCharCode; ++i < l;
            s[i] = (c = s[i].charCodeAt(0)) >= 127 ? o(0xc0 | (c >>> 6)) + o(0x80 | (c & 0x3f)) : s[i]
        );
        return s.join("");
    },
    decode: function (s) {
        for (var a, b, i = -1, l = (s = s.split("")).length, o = String.fromCharCode, c = "charCodeAt"; ++i < l;
            ((a = s[i][c](0)) & 0x80) &&
            (s[i] = (a & 0xfc) == 0xc0 && ((b = s[i + 1][c](0)) & 0xc0) == 0x80 ?
                o(((a & 0x03) << 6) + (b & 0x3f)) : o(128), s[++i] = "")
        );
        return s.join("");
    }
};

function edituser(userid) {
    localStorage.setItem('userid', userid);
    window.location.href = 'user.html';
}

document.getElementById("adduserbutton").addEventListener("click", async () => {
    window.location.href = 'newuser.html';
});

window.onload = function () {
    loadlistuser();
}    

