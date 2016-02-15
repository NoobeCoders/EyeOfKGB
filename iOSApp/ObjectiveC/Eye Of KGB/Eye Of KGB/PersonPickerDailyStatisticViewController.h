//
//  PersonPickerDailyStatisticViewController.h
//  Eye Of KGB
//
//  Created by bu on 07/02/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol PersonPickerViewControllerDelegate <NSObject>
- (void)setSelectedRowPersonPicker:(NSInteger)selectedRow andBoolForPersonButtonName: (BOOL)boolForPersonButtonName;
@end

@interface PersonPickerDailyStatisticViewController : UIViewController

@property (nonatomic, strong) id <PersonPickerViewControllerDelegate> delegate;

@end
