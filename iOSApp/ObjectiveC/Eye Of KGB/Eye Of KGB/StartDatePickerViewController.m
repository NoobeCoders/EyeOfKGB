//
//  StartDatePickerViewController.m
//  Eye Of KGB
//
//  Created by bu on 08/02/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "StartDatePickerViewController.h"

@interface StartDatePickerViewController ()
@property (strong, nonatomic) NSString *formatedDate;
@property (weak, nonatomic) IBOutlet UIDatePicker *startDatePicker;
@end

@implementation StartDatePickerViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    [self getDateFromDatePicker];
}

- (void) getDateFromDatePicker {
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    dateFormatter.dateFormat = @"yyyy-MM-dd";
    self.formatedDate = [dateFormatter stringFromDate:self.startDatePicker.date];
}

- (IBAction)cancelButton:(id)sender {
    [self dismissViewControllerAnimated:YES completion:nil];
}

- (IBAction)doneButton:(id)sender {
    [self getDateFromDatePicker];
    BOOL isItSelected = true;
    [self.delegate setStartDate:self.formatedDate andBoolForStartDateButtonName:isItSelected];
    [self dismissViewControllerAnimated:YES completion:nil];
}


@end
