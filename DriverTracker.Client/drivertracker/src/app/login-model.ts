export class LoginModel {
    input: Input;

    constructor(email: string, password: string) {
        this.input.email = email;
        this.input.password = password;
        this.input.rememberMe = false;
    }
}

export class Input {
    email: string;
    password: string;
    rememberMe: boolean;
}