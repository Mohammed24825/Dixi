import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

export interface LoginResponse {
  token: string;
  userId: string;
  fullName: string;
  role: 'Student' | 'Instructor' | 'Admin';
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private baseUrl = 'http://localhost:5032/api/auth'; 

  currentUser = signal<LoginResponse | null>(this.loadFromStorage());

  private loadFromStorage(): LoginResponse | null {
    const raw = localStorage.getItem('dixi-auth');
    return raw ? JSON.parse(raw) : null;
  }

  login(identifier: string, password: string) {
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, { identifier, password });
  }

  setSession(user: LoginResponse) {
    localStorage.setItem('dixi-auth', JSON.stringify(user));
    this.currentUser.set(user);
  }

  redirectByRole(role: string) {
    switch (role) {
      case 'Student': this.router.navigate(['/student', this.currentUser()?.userId]); break;
      case 'Instructor': this.router.navigate(['/instructor', this.currentUser()?.userId]); break;
      case 'Admin': this.router.navigate(['/admin', this.currentUser()?.userId]); break;
    }
  }

  logout() {
    localStorage.removeItem('dixi-auth');
    this.currentUser.set(null);
    this.router.navigate(['/login']);
  }
}