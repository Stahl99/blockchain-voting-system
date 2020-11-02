#!/usr/bin/expect -f
set timeout -1
spawn ./setup.sh
expect {
  "Do you want to continue?" {
    send -- "yes\r"
    exp_continue
  }
  "Do you want the jump plugin which follows symbolic links?" {
    send -- "no\r"
    exp_continue
  }
  "Do you want to create a User Config file?" {
    send -- "yes\r"
  }
}
