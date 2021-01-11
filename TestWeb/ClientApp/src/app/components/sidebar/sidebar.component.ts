import { Component, OnInit } from '@angular/core';

declare const $: any;
declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}
export const ROUTES: RouteInfo[] = [
  { path: '/explore', title: 'Explore', icon: 'dashboard', class: '' },
  { path: '/user-profile', title: 'My Profile', icon: 'person', class: '' },
  { path: '/collection', title: 'My Collections', icon: 'content_paste', class: '' },
  { path: '/logout', title: 'Logout', icon: 'exit_to_app', class: '' },
];

export const ADMINROUTES: RouteInfo[] = [
  { path: '/explore', title: 'Explore', icon: 'dashboard', class: '' },
  { path: '/admin-panel', title: 'Admin Panel', icon: 'https', class: '' },
  { path: '/logout', title: 'Logout', icon: 'exit_to_app', class: '' },
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor() { }

  ngOnInit() {
    var user = JSON.parse(localStorage.getItem('currentUser'));
    if (user.user.userName == "admin") {
      this.menuItems = ADMINROUTES.filter(menuItem => menuItem);
    }
    else {
      this.menuItems = ROUTES.filter(menuItem => menuItem);
    }
  }
  isMobileMenu() {
    if ($(window).width() > 991) {
      return false;
    }
    return true;
  };
}
