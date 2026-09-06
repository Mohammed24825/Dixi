import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface Instructor {
  id: string;
  fullName: string;
  title: string | null;
  bio: string | null;
  coursesCount: number;
}

@Injectable({ providedIn: 'root' })
export class InstructorService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:5032/api/instructors'; 

  getAll() {
    return this.http.get<Instructor[]>(this.baseUrl);
  }
}