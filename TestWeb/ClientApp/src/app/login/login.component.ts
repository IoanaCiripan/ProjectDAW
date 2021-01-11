import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { resetComponentState } from '@angular/core/src/render3/instructions';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [AuthService],
})
export class LoginComponent implements OnInit {

  // variables
  loginForm: FormGroup;
  registerForm: FormGroup;
  loginError: string = "";
  registerErrors: string[] = [];

  // initialisations
  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
  ) {
    localStorage.removeItem('currentUser');
  }

  ngOnInit() {
    localStorage.removeItem('currentUser');
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  // functions
  onSubmitRegister() {
    this.registerErrors = [];
    if (!this.validateEmail(this.registerForm.value.email)) {
      this.registerErrors.push("Invaild email");
    } else if (this.registerForm.value.password != this.registerForm.value.confirmPassword) {
      this.registerErrors.push("Cofrim password doesn't match the password");
    } else {
      this.authService.register({
        username: this.registerForm.value.email,
        password: this.registerForm.value.password
      }).subscribe(succ => {
        this.registerErrors = [];
        this.router.navigate(['/explore']);
      }, err => {
        var errorDescriptions = [];
        err.error.forEach(e => {
          errorDescriptions.push(e.description);
        });
        this.registerErrors = errorDescriptions;
      });
    }
  }

  validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }

  onSubmitLogin() {
    this.authService.login(this.loginForm.value).subscribe(succ => {
      this.loginError = "";
      this.router.navigate(['/explore']);
    }, err => {
      this.loginError = 'Invalid username or password';
    });
  }

}
