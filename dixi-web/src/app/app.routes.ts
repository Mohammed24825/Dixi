import { Routes } from '@angular/router';
import { LoadingComponent } from './pages/loading/loading';
import { LandingComponent } from './pages/landing/landing';
import { Login } from './pages/login/login';
import { StudentDashboard } from './pages/student-dashboard/student-dashboard';
import { InstructorDashboard } from './pages/instructor-dashboard/instructor-dashboard';
import { AdminDashboard } from './pages/admin-dashboard/admin-dashboard';
import { roleGuard } from './core/Auth/role-guard';
import { ManageUsers } from './pages/admin-dashboard/manage-users/manage-users';
import { ManageCourses } from './pages/admin-dashboard/manage-courses/manage-courses';

export const routes: Routes = [
  { path: '', component: LoadingComponent },
  { path: 'home', component: LandingComponent },
  { path: 'login', component: Login },
  { path: 'student/:id', component: StudentDashboard, canActivate: [roleGuard('Student')] },
  { path: 'instructor/:id', component: InstructorDashboard, canActivate: [roleGuard('Instructor')] },
  {
    path: 'admin/:id',
    component: AdminDashboard,
    canActivate: [roleGuard('Admin')],
    children: [
      { path: 'users', component: ManageUsers },
      { path: 'courses', component: ManageCourses },
      { path: '', redirectTo: 'users', pathMatch: 'full' },
    ]
  },
  { path: '**', redirectTo: '' },
];