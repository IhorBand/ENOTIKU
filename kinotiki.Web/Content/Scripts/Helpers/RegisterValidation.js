
function isValidLogin(login) {
    let ret = { Ok: false, message: "" };
    if (login != null && login.length != null && login.length > 0) {
        if(login.length > 12) {
            ret.message = "Login Cannot contain more than 12 characters.";
            return ret;
        }

        ret.Ok = true;
        return ret;
    } else {
        ret.message = "Login Cannot be Empty." ;
        return ret;
    }
}

function isValidPass(pass) {
    let ret = { Ok: false, message: "" };
    if (pass != null && pass.length != null && pass.length > 0) {
        ret.Ok = true;
        return ret;
    }
    ret.message = "Birthday cannot be null";
    return ret;
}

function isEmail(email) {
    let ret = { Ok: false, message: "" };
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        ret.message = "Wrong Email.";
        return ret;
    } else {
        ret.Ok = true;
        return ret;
    }
}

function isValidBirthday(birthday) {
    let ret = { Ok: false, message: "" };
    if (birthday != null && birthday.length != null && birthday.length > 0) {
        if (birthday.match(/[a-z]/i)) {
            ret.message = "Enter Valid Birthday.";
            return ret;
        }
        ret.Ok = true;
        return ret;
    }
    ret.message = "Birthday cannot be null";
    return ret;
}

