import { Component, inject, signal, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterLink } from '@angular/router';

interface AdminCourse {
  id: string;
  title: string;
  instructorName: string | null;
  isRemoved: boolean;
  studentCount: number;
  sessionCount: number;
}

@Component({
  selector: 'app-manage-courses',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './manage-courses.html',
  styleUrl: './manage-courses.scss',
})
export class ManageCourses implements OnInit {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:5032/api/admin/courses'; 

  courses = signal<AdminCourse[]>([]);

  ngOnInit() { this.load(); }

  load() {
    this.http.get<AdminCourse[]>(this.baseUrl).subscribe(data => this.courses.set(data));
  }

  removeCourse(course: AdminCourse) {
    if (!confirm(`Remove "${course.title}"? It will be hidden from listings but kept in history.`)) return;
    this.http.delete(`${this.baseUrl}/${course.id}`).subscribe(() => this.load());
  }
}