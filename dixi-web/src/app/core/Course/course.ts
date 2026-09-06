import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface Course {
  id: string;
  title: string;
  description: string;
  price: number;
  durationWeeks: number;
  level: string;
  instructorName: string;
}

@Injectable({ providedIn: 'root' })
export class CourseService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:5032/api/courses';

  getAll() {
    return this.http.get<Course[]>(this.baseUrl);
  }
}