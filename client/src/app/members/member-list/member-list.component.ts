import { take } from 'rxjs/operators';
import { AccountService } from './../../_services/account.service';
import { UserParams } from './../../_models/userParams';
import { Observable } from 'rxjs';
import { MembersService } from './../../_services/members.service';
import { Member } from './../../_models/member';
import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members: Member[];
  pagination: Pagination; 
  UserParams: UserParams; 
  user: User;

  constructor(private memberService: MembersService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => { 
      this.user = user; 
      this.UserParams = new UserParams(user); 
    })    
   }

  ngOnInit(): void {
   this.loadMembers(); 
  }

  loadMembers()
  { 
    this.memberService.getMembers(this.UserParams).subscribe(response => { 
      this.members = response.result; 
      this.pagination = response.pagination; 
    })
  }

  pageChanged(event: any)
  { 
    this.UserParams.pageNumber = event.page; 
    this.loadMembers();
  }

}
