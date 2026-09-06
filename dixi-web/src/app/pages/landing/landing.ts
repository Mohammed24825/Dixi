import { Component, inject, signal, OnInit } from '@angular/core';
import { NavbarComponent } from '../../shared/navbar/navbar';
import { InstructorService, Instructor } from '../../core/Instructor/instructor';
import { LucideAngularModule, Code, Users,BarChart, Clock, User, BookOpen, ArrowRight  } from 'lucide-angular';
import { CourseService, Course } from '../../core/Course/course';
import { RouterLink } from '@angular/router'; // 1. Add this import



@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [NavbarComponent, LucideAngularModule, RouterLink],
  templateUrl: './landing.html',
  styleUrl: './landing.scss',
})
export class LandingComponent implements OnInit {
  private instructorService = inject(InstructorService);
  readonly CodeIcon = Code;
  readonly UsersIcon = Users;

  private courseService = inject(CourseService);
  readonly BarChartIcon = BarChart;
  readonly ClockIcon = Clock;
  readonly UserIcon = User;
  readonly BookOpenIcon = BookOpen;
  readonly ArrowRightIcon = ArrowRight;

  instructors = signal<Instructor[]>([]);
  courses = signal<Course[]>([]);



  ngOnInit() {
    this.instructorService.getAll().subscribe({
      next: (data) => this.instructors.set(data),
      error: (err) => console.error('Failed to load instructors', err),
    });
    this.courseService.getAll().subscribe({
      next: (data) => this.courses.set(data),
      error: (err) => console.error('Failed to load courses', err),
    });
  }
}