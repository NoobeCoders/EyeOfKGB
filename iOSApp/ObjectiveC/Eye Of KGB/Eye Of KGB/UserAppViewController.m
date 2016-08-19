//
//  UserAppViewController.m
//  Eye Of KGB
//
//  Created by bu on 23/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "UserAppViewController.h"
#import "DailyStatisticViewController.h"

@interface UserAppViewController ()

@property (weak, nonatomic) IBOutlet UIView *generalStatisticView;
@property (weak, nonatomic) IBOutlet UIView *dailyStatisticView;
- (IBAction)segmentControl:(id)sender;

@end

@implementation UserAppViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}


- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)segmentControl:(id)sender {
    UISegmentedControl *segment = sender;
    
    switch (segment.selectedSegmentIndex) {
        case 0:
            self.generalStatisticView.hidden = NO;
            break;
        case 1:
            self.generalStatisticView.hidden = YES;
            break;
        default:
            break;
    }
}
@end
