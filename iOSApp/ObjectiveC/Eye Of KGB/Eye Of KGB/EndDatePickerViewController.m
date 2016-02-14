//
//  EndDatePickerViewController.m
//  Eye Of KGB
//
//  Created by bu on 08/02/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "EndDatePickerViewController.h"

@interface EndDatePickerViewController ()
@property (strong, nonatomic) NSString *formatedDate;
@property (weak, nonatomic) IBOutlet UIDatePicker *endDatePicker;
@end

@implementation EndDatePickerViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    [self getDateFromDatePicker];
}

- (void) getDateFromDatePicker {
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    dateFormatter.dateFormat = @"yyyy-MM-dd";
    self.formatedDate = [dateFormatter stringFromDate:self.endDatePicker.date];
}

- (IBAction)cancelButton:(id)sender {
    [self dismissViewControllerAnimated:YES completion:nil];
}

- (IBAction)doneButton:(id)sender {
    [self getDateFromDatePicker];
    BOOL isItSelected = true;
    [self.delegate setEndDate:self.formatedDate andBoolForEndDateButtonName:isItSelected];
    [self dismissViewControllerAnimated:YES completion:nil];
}
@end
