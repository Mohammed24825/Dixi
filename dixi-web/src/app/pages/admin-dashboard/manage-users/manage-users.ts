import { Component, inject, signal, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterLink } from '@angular/router';

interface AdminUser {
  id: string;
  fullName: string;
  role: string;
  isSuspended: boolean;
}

@Component({
  selector: 'app-manage-users',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './manage-users.html',
  styleUrl: './manage-users.scss',
})
export class ManageUsers implements OnInit {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:5032/api/admin/users'; 

  users = signal<AdminUser[]>([]);

  ngOnInit() { this.load(); }

  load() {
    this.http.get<AdminUser[]>(this.baseUrl).subscribe(data => this.users.set(data));
  }

  suspend(user: AdminUser) {
    if (user.role === 'Admin') return;
    this.http.post(`${this.baseUrl}/${user.id}/suspend`, {}).subscribe(() => this.load());
  }
}