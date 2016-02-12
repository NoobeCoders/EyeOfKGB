//
//  PersonPickerDailyStatisticViewController.m
//  Eye Of KGB
//
//  Created by bu on 07/02/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "PersonPickerDailyStatisticViewController.h"
#import "GetData.h"

@interface PersonPickerDailyStatisticViewController () <UIPickerViewDataSource, UIPickerViewDelegate>

@property (strong, nonatomic) GetData *data;
@property (weak, nonatomic) IBOutlet UIPickerView *sitePicker;
@property (assign, nonatomic) NSInteger selectedRow;

@end

@implementation PersonPickerDailyStatisticViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    self.sitePicker.delegate = self;
    self.sitePicker.dataSource = self;
    
    self.data = [[GetData alloc] init];
    [self.data getNames];
}

//MARK: - Delegates and datasource (pickerview)

// returns the number of 'columns' to display.
- (NSInteger)numberOfComponentsInPickerView:(UIPickerView *)pickerView {
    return 1;
}

// returns the # of rows in each component..
- (NSInteger)pickerView:(UIPickerView *)pickerView numberOfRowsInComponent:(NSInteger)component {
    return self.data.names.count;
}

- (NSString *)pickerView:(UIPickerView *)thePickerView titleForRow:(NSInteger)row forComponent:(NSInteger)component {
    return [self.data.names objectAtIndex:row];
}

- (IBAction)cancelButton:(id)sender {
    [self dismissViewControllerAnimated:YES completion:nil];
}

- (IBAction)doneButton:(id)sender {
    BOOL isItSelected = true;
    self.selectedRow = [self.sitePicker selectedRowInComponent:0];
    [self.delegate setSelectedRowPersonPicker:self.selectedRow andBoolForPersonButtonName:isItSelected];
    [self dismissViewControllerAnimated:YES completion:nil];
}


@end
