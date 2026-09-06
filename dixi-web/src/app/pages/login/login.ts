import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Mail, Lock, LogIn, Eye, EyeOff } from 'lucide-angular';
import { NavbarComponent } from '../../shared/navbar/navbar';
import { AuthService } from '../../core/Auth/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NavbarComponent,
    LucideAngularModule,
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);

  // Lucide Icons
  readonly MailIcon = Mail;
  readonly LockIcon = Lock;
  readonly LogInIcon = LogIn;
  readonly EyeIcon = Eye;
  readonly EyeOffIcon = EyeOff;

  // UI State
  showPassword = signal<boolean>(false);
  errorMessage = signal<string>('');

  form = this.fb.group({
    identifier: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });

  togglePasswordVisibility(): void {
    this.showPassword.update((show) => !show);
  }

  submit(): void {
    if (this.form.invalid) return;

    this.errorMessage.set('');
    const { identifier, password } = this.form.value;

    this.auth.login(identifier!, password!).subscribe({
      next: (res) => {
        this.auth.setSession(res);
        this.auth.redirectByRole(res.role);
      },
      error: () => {
        this.errorMessage.set('Invalid email/phone or password.');
      },
    });
  }
}