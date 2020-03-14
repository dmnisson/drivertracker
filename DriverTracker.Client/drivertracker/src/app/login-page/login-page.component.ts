import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginModel } from '../login-model';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.sass']
})
export class LoginPageComponent implements OnInit {

  model = new LoginModel('', '');

  showBadCredentialError = false;
  showGenericError = false;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  signIn() {
    // reset errors
    this.showBadCredentialError = false;
    this.showGenericError = false;

    this.authService.makeToken(
      this.model.input.email,
      this.model.input.password
    ).then(() => this.router.navigateByUrl('/'))
      .catch(err => {
        switch (err.status) {
          case 401:
            this.showBadCredentialError = true;
            break;
          default:
            this.showGenericError = true;
        }
      });
  }

}
