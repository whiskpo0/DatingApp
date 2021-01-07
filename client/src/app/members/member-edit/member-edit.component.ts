import { ToastrService } from 'ngx-toastr';
import { AccountService } from './../../_services/account.service';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { MembersService } from 'src/app/_services/members.service';
import { take } from 'rxjs/operators';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm; 

  member: Member;
  user: User;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) { // this keeps uyou from leaving the page/site without losing your data. 
    if (this.editForm.dirty) {
      $event.returnValue = true; 
    }
  }

  constructor(private accountService: AccountService, private memberService: MembersService, private toastr: ToastrService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user); 
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember()
  { 
    this.memberService.getMember(this.user.username).subscribe(member => { 
      this.member = member; 
    })
  }

  updateMember()
  { 
    this.memberService.updateMember(this.member).subscribe(() => { 
      this.toastr.success('Profile Updated Successfuly');  
      this.editForm.reset(this.member); 
    })  
  }
}
